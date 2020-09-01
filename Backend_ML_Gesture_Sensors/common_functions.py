#!/usr/bin/env python
# coding: utf-8

# # Imports / Definitions

# In[1]:


import re
import csv
import glob
import os
import datetime
import pandas as pd
import numpy as np
import math
from sklearn.neural_network import MLPClassifier
from sklearn.model_selection import GridSearchCV
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report
import matplotlib.pyplot as plt
import missingno as mso
import joblib
import time

def read_activities_from_folder(path, activity_id_begin, data_set_description):

    activity_id=activity_id_begin
    activity_list=[]
    value_list=[]
    longterm=None
    
    #iterate over all files and read meta/sensor data
    for filename in glob.glob(path + '**/*gear_s*.csv', recursive=True):
        if os.path.isfile(filename):
            print('Parsing activity '+str(activity_id)+' from file '+os.path.basename(filename))
            # check for longterm records and set flag (all created longterm files start with _gear) 
            if os.path.basename(filename).startswith('_gear_s'):
                longterm=True
            else:
                longterm=False
            activity, values,activity_id=read_activity_file(filename, activity_id, data_set_description, longterm)
            activity_list.extend(activity)
            value_list.extend(values)
        else:
            print('File not found ('+filename+')')

    if len(activity_list)>0:
        #convert to pandas dataframe
        df_activities=pd.DataFrame(activity_list)
        df_values=pd.DataFrame(value_list)

        #assign column names
        df_activities.columns=['activity_id','activity_label','recording_timestamp','user',                               'file_name','file_format','data_set','longterm','noised']
        df_values.columns=['activity_id','timestamp', 'sensor','X','Y','Z']
        
        #convert datatypes (reduce memory footprint)
        df_activities['activity_id']=df_activities['activity_id'].astype('int32')
        df_activities['activity_label']=df_activities['activity_label'].astype('category')
        df_activities['recording_timestamp']=df_activities['recording_timestamp'].astype('datetime64[ns]')
        df_activities['user']=df_activities['user'].astype('category')
        df_activities['file_name']=df_activities['file_name'].astype('category')
        df_activities['file_format']=df_activities['file_format'].astype('category')
        df_activities['data_set']=df_activities['data_set'].astype('category')
        df_activities['longterm']=df_activities['longterm'].astype('bool')
        df_activities['noised']=df_activities['noised'].astype('bool')

        df_values['activity_id']=df_values['activity_id'].astype('int32')
        df_values['timestamp']=df_values['timestamp'].astype('datetime64[ns]')
        df_values['sensor']=df_values['sensor'].astype('category')
    else:
        print('Path contains no .csv files('+path+')')
    return df_activities,df_values, activity_id


def read_activity_file(filename, activity_id, data_set, longterm):
    
    #sensors that are currently supported, excluding GP and GM because of high missing values
    allowed_sensors=['GR','GA','GM','GP']
    i=0
    with open(filename, 'r') as csvfile:
        reader = csv.reader(csvfile, delimiter=';', quotechar='|')
        activity_label=None
        device=None
        user=None
        activity=[]
        values=[]
        file_format=None
        noised = 0
        timing = datetime.timedelta(seconds=12)
        
        #iterate over all rows in the csv file
        for row in reader:
            #Extract specific meta data from first row(varies from the used wearables)
            if(i<1):            
                if(len(row)==9):
                    file_format='header_9'
                    user=row[8]
                elif(len(row)==10):
                    #if number of columns = 10: 
                    #ignore value, ignore value, time stamp, ADL recorded, user name, ignore value
                    file_format='header_10'
                    user=row[8]
                else:
                    #if number of columns = 11: 
                    #ignore value, ignore value, time stamp, sensor type, right/left handed 
                    #ADL recorded, super ADL, user name, ignore value
                    file_format='header_11'
                    user=row[9]
                #Extract general meta data from sensor values
                activity_label=row[7]

#---Spezielle Studentenergänzung für die Bewertug von Trinkmengen
#---ergänzung: hier wird nun das Label, das sich in abhängigkeit des Ordners definiert
                #if activity_label == 'Drink':
                    #dir = os.path.basename(os.path.dirname(filename))
                    #if dir:
                        #drink_ml = activity_label + dir
                        #drink_ml = drink_ml[:-2]
                        #activity_label = activity_label.replace('Drink', drink_ml)
#---
                rec_timer=pd.to_datetime(datetime.datetime.fromtimestamp(int(row[2])/1e3))
                threshold = rec_timer + timing
                if longterm == True:
                    activity.append([activity_id,'Longterm',rec_timer,user,                                     os.path.basename(filename),                                     file_format,data_set,longterm, noised])
                else:
                    activity.append([activity_id,activity_label,rec_timer,user,                                     os.path.basename(filename),file_format,data_set,longterm, noised])
                  
            else:
                if(len(row)>6):
                    #Extract sensor values
                    sensor_type=row[3]
                    if(sensor_type in allowed_sensors):
                        #get timestamp of each row
                        time_stamp = pd.to_datetime(datetime.datetime.fromtimestamp(int(row[2])/1e3))
                        #if file is longterm and iteration crossed 12 seconds, 
                        #a new activity is created with timestamp of threshold
                        if (longterm==True and (time_stamp>threshold)):
                            if (time_stamp>threshold+timing):
                                while((time_stamp-timing)>threshold):
                                    threshold += timing
                            activity_id+=1
                            activity.append([activity_id,'Longterm',threshold,user,                                             os.path.basename(filename),                                             file_format, data_set, longterm, noised])
                            threshold += timing
                        if(sensor_type =='GA'):
                            values.append([activity_id,time_stamp,sensor_type,                                           float(row[4]),float(row[5]),float(row[6])])
                            values.append([activity_id,time_stamp,'GX',                                           float(row[7]),float(row[8]),float(row[9])])
                        else:
                            values.append([activity_id,time_stamp,sensor_type,                                           float(row[4]),float(row[5]),float(row[6])])
                            
            i+=1   
    activity_id+=1
    return activity, values, activity_id


def preprocess_activity_dataframe(df):
    df['user']=df['user'].str.lower()
    df['user']=df['user'].str.rstrip()
    df['activity_label']=df['activity_label'].str.lower()
    return df
    

def get_features(activity_id,sensor,data):
    # sensor features per sensor and per axis
    sensor_val = data[['X','Y','Z']]
    sensor_features = {}
    for index, col in enumerate(sensor_val.columns):
        sensor_features[sensor+'_'+col+'_'+'MIN']=sensor_val[col].min()
        sensor_features[sensor+'_'+col+'_'+'MAX']=sensor_val[col].max()
        sensor_features[sensor+'_'+col+'_'+'STD']=sensor_val[col].std()
        sensor_features[sensor+'_'+col+'_'+'MEA']=sensor_val[col].mean()
        sensor_features[sensor+'_'+col+'_'+'MED']=sensor_val[col].median()
        sensor_features[sensor+'_'+col+'_'+'IQR']=(sensor_val[col].quantile(.75)-sensor_val[col].quantile(.25))
        sensor_features[sensor+'_'+col+'_'+'ZCR']=get_zero_crossings(sensor_val[col])
    return sensor_features
    del sensor_features

def get_zero_crossings(values):
    b=0
    if len(values) < 3:
        return b
    for a in (values):
        if (a>0 and a+1<=0):
            b=b+1
        elif a<=0 and a+1>0:
            b=b+1
    return b

def create_features(df_single, sensors, activity_id):
    features=pd.DataFrame()
    # pass single sensor values for feature calculation
    for sensor in sensors:
        df_sensor=df_single.loc[df_single.sensor==sensor]
        sens_features = pd.DataFrame([get_features(activity_id,sensor,df_sensor)])
        features = pd.concat([features,sens_features],axis=1, sort=True)
        temp = df_sensor['X']**2 + df_sensor['Y']**2 + df_sensor['Z']**2
        features[sensor+'_QMW'] =  math.sqrt(temp.sum()/len(df_sensor['Y']))
    return features
    del features
    
def process_multiple_activities(activity, data, store, mlp_store):
    df_copy = pd.DataFrame.copy(data)
    df_copy.loc[(df_copy['activity'] != activity), 'activity'] = 'other'
    # Prepare Data
    #df_train = df_copy.drop(['activity', 'activity_id'], axis=1)
    df_train = df_copy.drop(['activity','data_set'], axis=1)
    df_train_label = df_copy['activity']
    data_train, data_test, labels_train, labels_test = train_test_split(df_train, df_train_label, test_size=0.33, random_state=0, shuffle=True, stratify=df_train_label)

    # Set hyper parameters
    param_grid = {'learning_rate_init': [0.001,0.003,0.005,0.007,0.009,0.01,0.02,0.03],                  'activation':['relu']}

    # Hyper parameter tuning with grid search and 10-fold cross-validation
    mlp = GridSearchCV(MLPClassifier(activation='relu', max_iter=500), param_grid, cv=10, return_train_score=True, scoring='accuracy')

    # Start training
    print('Start training', activity, datetime.datetime.now())
    mlp.fit(data_train, labels_train)

    # Show some stats from CV   
    print('Stop training', activity, datetime.datetime.now())
    print('Best params of model:', mlp.best_params_)
    print('Scores Train for '+activity+': %0.5f (+/- %0.4f)' % (max(mlp.cv_results_['mean_train_score'])*100,mlp.cv_results_['mean_train_score'].std()*100))
    print('Scores Validation for '+activity+': %0.5f (+/- %0.4f)' % (max(mlp.cv_results_['mean_test_score'])*100,mlp.cv_results_['mean_test_score'].std()*100))

    # Evaluate best model
    predictions = mlp.predict(data_test)

    # Return score of predictions
    score = mlp.score(data_test, labels_test)
    print('Accuracy on Test for '+activity+': %0.5f' % (score*100), '\n')

    # Return classification result (f1 report)
    classified = classification_report(labels_test, predictions)

    # Write results
    pd.DataFrame(mlp.cv_results_).to_csv(os.path.join(store,'MLP-training-result-'+activity+'-'+time.strftime('%Y%m%d-%H%M%S')+'.csv'), index=False)
    f = open(os.path.join(store,'MLP-classification-result-'+activity+'-'+time.strftime('%Y%m%d-%H%M%S')+'.txt'),'w'); f.write(classified); f.close()
    # store model
    joblib.dump(mlp.best_estimator_,os.path.join(mlp_store,'Model-MLP-'+activity+'.sav'))
    del df_copy, df_train, df_train_label, mlp

def process_activity(data, store, mlp_store):
    df_copy = pd.DataFrame.copy(data)
    # Prepare Data
    df_train = df_copy.drop(['activity'], axis=1)
    df_train_label = df_copy['activity']
    data_train, data_test, labels_train, labels_test = train_test_split(df_train, df_train_label, test_size=0.33, random_state=0, shuffle=True, stratify=df_train_label)

    # Set hyper parameters
    param_grid = {'learning_rate_init': [0.001,0.003,0.005,0.007,0.009,0.01,0.02,0.03],                  'activation':['relu']}

    # Hyper parameter tuning with grid search and 10-fold cross-validation
    mlp = GridSearchCV(MLPClassifier(activation='relu', max_iter=500), param_grid, cv=10, return_train_score=True, scoring='accuracy')

    # Start training
    print('Start training', datetime.datetime.now())
    mlp.fit(data_train, labels_train)

    # Show some stats from CV   
    print('Stop training', datetime.datetime.now())
    print('Best params of model:', mlp.best_params_)
    print('Scores Train: %0.5f (+/- %0.4f)' % (max(mlp.cv_results_['mean_train_score'])*100,mlp.cv_results_['mean_train_score'].std()*100))
    print('Scores Validation: %0.5f (+/- %0.4f)' % (max(mlp.cv_results_['mean_test_score'])*100,mlp.cv_results_['mean_test_score'].std()*100))

    # Evaluate best model
    predictions = mlp.predict(data_test)

    # Return score of predictions
    score = mlp.score(data_test, labels_test)
    print('Accuracy on Test: %0.5f' % (score*100), '\n')

    # Return classification result (f1 report)
    classified = classification_report(labels_test, predictions)

    # Write results
    pd.DataFrame(mlp.cv_results_).to_csv(os.path.join(store,'MLP-training-result-'+time.strftime('%Y%m%d-%H%M%S')+'.csv'), index=False)
    f = open(os.path.join(store,'MLP-classification-result-'+time.strftime('%Y%m%d-%H%M%S')+'.txt'),'w'); f.write(classified); f.close()
    # store model
    joblib.dump(mlp.best_estimator_,os.path.join(mlp_store,'Model-MLP.sav'))
    del df_copy, df_train, df_train_label, mlp
    
def noising(df_activity, df_single, new_id):
    # define empty pandas for values and activity to be returned
    dfs_noised_act, dfs_noised_val = pd.DataFrame(),pd.DataFrame()
    
    # loop 10 times to apply random noise
    for i in range(0,10):
        
        # definition of median 'mu' and deviation random 'sigma' within defined range
        # create numpy array with gaussian noise for X,Y and Z with the same row length of each single activity
        mu, sigma = 0, np.random.uniform(0.01, 0.07)
        noise = np.random.normal(mu, sigma, [len(df_single.index),3])

        # split array into activity attributes and raw data and add noised numpy array
        # set new activity_id for values and activity
        df_noised = pd.concat([df_single[['activity_id','timestamp','sensor']],                               pd.eval("df_single[['X','Y','Z']] + noise")], axis=1, join='outer')
        df_noised['activity_id']=new_id
        df_activity.activity_id, df_activity.noised = new_id, True

        # append activity and values
        dfs_noised_act, dfs_noised_val = dfs_noised_act.append(df_activity), dfs_noised_val.append(df_noised)
        # increment activity_id
        new_id += 1
    return dfs_noised_act, dfs_noised_val, new_id
    del dfs_noised_act, dfs_noised_val

def build_dataset(df_act,df_val,new_id):
    # define empty pandas for values and activity to be returned
    dfs_final_act, dfs_final_val = pd.DataFrame(),pd.DataFrame()
    
    # take one activity and respective values out of file and calculate noised features
    for activity_id in (df_act.activity_id.values):   
        df_gauss_act, df_gauss_val, new_id=noising(pd.Series.copy(df_act.loc[df_act.activity_id==activity_id]),pd.Series.copy(df_val.loc[df_val.activity_id==activity_id]),new_id)
        
        dfs_final_act = pd.concat([dfs_final_act,df_gauss_act], ignore_index=True)
        dfs_final_val = pd.concat([dfs_final_val,df_gauss_val], ignore_index=True)
        
        print(df_gauss_act.activity_id.count(), df_gauss_act.activity_label.unique(), 'activities added with last activity_id', new_id-1)
    return dfs_final_act,dfs_final_val, new_id
    del dfs_final_act,dfs_final_val
    
def display_missing(df_features):
    for dataset in df_features.data_set.unique():
        df = df_features.loc[df_features.data_set == dataset]
        missing_df=df.columns[df.isnull().any()].tolist()
        if not len(missing_df) ==0:
            print('Missing Values of', dataset)
            mso.matrix(df[missing_df],labels=True,sparkline=True,width_ratios=(40,1), figsize=(45, 10)); plt.show()
            mso.bar(df[missing_df]); plt.show()
        else:
            print('No missing values in', dataset)

def display_dataset(df_activities):
    df_activities.activity.value_counts().plot.barh(figsize=(12,6),title='Anzahl der aufgenommenen Tätigkeiten innerhalb der Datasets')
    # Overview about activities in datasets
    df_activities.groupby(['data_set', 'activity']).data_set.count().unstack().plot.bar(legend=True, stacked=True,figsize=(12,10),title='Verteilung der Datensätze in den Datasets')
    
    #figure, axes = plt.subplots(nrows=2, ncols=2, figsize=(12,12))
    for dataset in df_activities.data_set.unique():
        df_activities[df_activities.data_set==dataset].groupby(['activity']).activity.count().plot(kind='bar', title=dataset)
        plt.show()

