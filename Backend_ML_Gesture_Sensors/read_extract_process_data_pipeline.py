#!/usr/bin/env python
# coding: utf-8

# In[1]:


import datetime
import pandas as pd
import joblib
import shutil, os
import glob
from sklearn.neural_network import MLPClassifier
from sklearn.preprocessing import RobustScaler
from ipynb.fs.defs.common_functions import             read_activities_from_folder,preprocess_activity_dataframe,            create_features,process_multiple_activities,build_dataset,            display_missing, display_dataset


# In[2]:


# define the activities (or single activity)to test within a array e.g. ['wash', 'tumble']
test_activities = ['wash']


# In[3]:


# Get root directory
os.chdir('E:/_Waldhoer/')


# In[4]:


log_store = os.path.abspath('./Logs_Training/')
mlp_store = os.path.abspath('./Trained_Models/')
feature_file= os.path.abspath('./Data/data_pool.pkl/')
processed_data_path= os.path.abspath('./Data/processed_data/')
new_project_data=os.path.abspath('/_Waldhoer/Imports/')


# In[5]:


#Get project related data directory to import new data into data pool
##(PROJECT FOLDER NAMED EQAL TO data_set)##
data_set='Waldhoer_Wash_2020'
data_path=os.path.join(new_project_data,data_set,'')


# # read data from files

# In[6]:


# get highest activity_id from last pickle and increment by +1 to use as new ID
df_old_features=pd.read_pickle(feature_file)
activity_id = df_old_features.tail(1).index.item()+1
print('last activity id: ', activity_id-1, '\n','new activity id: ', activity_id)


# In[7]:


if ([f for f in glob.glob(data_path + "**/*.csv", recursive=True)]):
    df_activities,df_values,activity_id=read_activities_from_folder(data_path,activity_id,data_set)
    df_activities=preprocess_activity_dataframe(df_activities)
    
    ### --- noise new activities --- ###
    # get new activity_id for noised data
    new_id = (df_activities['activity_id'].iloc[-1]) +1


    print('Start Noising: ',datetime.datetime.now())

    # call function to build noise dataset
    df_noised_act, df_noised_val, new_id = build_dataset(df_activities, df_values,new_id)

    # merge noised with origin dataset
    df_activities = df_activities.append(df_noised_act)
    df_values = df_values.append(df_noised_val)
    print('End Noising: ',datetime.datetime.now())
    print(new_id-activity_id, '# artificial activities created')
    
    ### --- extract feautures --- ###
    
    # get all included sensors
    sensors=list(df_values.sensor.values.unique())
    temp, new_act = pd.DataFrame(), pd.DataFrame()

    # loop for each activity id
    for activity_id in (df_activities.activity_id.values):

        # take one activity file and calculate features, set activity_id and activity_label
        new_act = create_features(df_values.loc[df_values.activity_id==activity_id], sensors, activity_id)
        new_act['activity_id']=activity_id
        new_act['activity']=(df_activities.loc[df_activities.activity_id==activity_id]).activity_label.values
        new_act['data_set']=data_set
        new_act = new_act.set_index('activity_id')

        print('Features created for', new_act.activity.values, '#', activity_id)
        # concat calculated features
        temp = temp.append(new_act)
        
    ### --- merge new activities and store updated data pool --- ###
    
    if (temp.data_set.unique() == 'Waldhoer_Wash_2020' and temp.activity.unique() == 'longterm'):
        temp['activity']=temp['activity'].replace({'longterm':'wash'})
    
    final = df_old_features.append(temp, sort=True)
    final.to_pickle(feature_file)
    
    ### --- moved processed files --- ###
    
    new_path = os.path.join(processed_data_path, data_set)
    if not os.path.exists(new_path):
        os.mkdir(new_path)
    for file in os.listdir(data_path):
        shutil.move(os.path.join(data_path,file), new_path) 
    
else:    
    print("Directory is empty, no data is being processed. Load existing data pool")
    final = df_old_features


# # display data

# In[8]:


display_dataset(final)
print(final.activity.unique())


# # clean and prepare data

# In[9]:


final = final[final.columns[~pd.Series(final.columns).str.match(pat='(^GM)|(^GP)')]]
final = final.drop(['GA_X_MED', 'GA_Y_MED', 'GA_Z_MED', 'GR_X_STD', 'GX_QMW', 'GX_X_STD', 'GX_Y_IQR', 'GX_Y_MAX', 'GX_Y_MIN', 'GX_Y_STD', 'GX_Z_MAX', 'GX_Z_MIN'], axis=1).dropna()


# In[10]:


display_missing(final)


# In[11]:


final = final.loc[~((final['activity'] == 'longterm') | (final['activity'] == 'tumble'))]
final['activity']=final['activity'].replace({'hwash':'wash'})
print(final.activity.unique())


# In[12]:


for col in list(final.columns[~final.columns.isin(['activity','data_set'])]):
    scaler = RobustScaler()
    final[[col]] = scaler.fit_transform(final[[col]])


# # train model for each activity

# In[13]:


for act in (test_activities):   
    process_multiple_activities(act,final,log_store,mlp_store)
#process_activity(final,log_store,mlp_store)

