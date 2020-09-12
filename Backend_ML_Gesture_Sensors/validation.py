import json
import csv
import datetime
import pandas as pd
import joblib, os
from sklearn.neural_network import MLPClassifier
import math

def get_prediction(jsonDict):
    #(C) 2020 Florian Full
    pd.set_option('display.max_rows', 1000)
    pd.set_option('display.max_columns', 110)

    parsedValues = []

    for gesturePoint in jsonDict["GesturePointList"]:
        time_stamp = pd.to_datetime(datetime.datetime.fromtimestamp(int(gesturePoint["TimeStamp"]) / 10000))
        parsedValues.append([1, time_stamp, 'GA', float(gesturePoint["Accelerometer"]["X"]), float(gesturePoint["Accelerometer"]["Y"]), float(gesturePoint["Accelerometer"]["Z"])])
        parsedValues.append([1, time_stamp, 'GR', float(gesturePoint["Gyroscope"]["X"]), float(gesturePoint["Gyroscope"]["Y"]), float(gesturePoint["Gyroscope"]["Z"])])

    parsedValuesList = []
        
    parsedValuesList.extend(parsedValues)
    
    #convert to pandas dataframe
    dfParsedValues = pd.DataFrame(parsedValuesList)
    
    #assign column names
    dfParsedValues.columns = ['id', 'timestamp', 'sensor', 'X', 'Y', 'Z']
            
    #convert datatypes (reduce memory footprint)  
    dfParsedValues['id'] = dfParsedValues['id'].astype('int32')
    dfParsedValues['timestamp'] = dfParsedValues['timestamp'].astype('datetime64[ns]')
    dfParsedValues['sensor'] = dfParsedValues['sensor'].astype('category')

    dfActivity = pd.DataFrame()

    for sensor in ('GA', 'GR'):
        dfSensor = dfParsedValues.loc[dfParsedValues.sensor == sensor]
        dfFeatures = pd.DataFrame([get_features(sensor, dfSensor)])
        dfActivity = pd.concat([dfActivity, dfFeatures], axis = 1, sort = True)
        temp = dfSensor['X']**2 + dfSensor['Y']**2 + dfSensor['Z']**2
        dfActivity[sensor + '_QMW'] =  math.sqrt(temp.sum() / len(dfSensor['Y']))
        
    dfActivity['id'] = 1
    dfActivity['activity'] = ''

    # concat calculated features,
    final = pd.DataFrame()
    final = pd.concat([final, dfActivity], ignore_index = True, sort = True)

    final = final[final.columns[~pd.Series(final.columns).str.match(pat = '(^GM)|(^GP)|(^GX)')]]
    final = final.drop(['GA_X_MED', 'GA_Y_MED', 'GA_Z_MED', 'GR_X_STD', ], axis=1).dropna()

    # Validate Data by Models

    modelDrink = joblib.load('Model-MLP-drink.sav')
    dataDrink = pd.DataFrame.copy(final)
    dataDrink = dataDrink.drop(['activity','id'], axis = 1)
    predictionDrink = modelDrink.predict(dataDrink)

    modelWash = joblib.load('Model-MLP-wash.sav')
    dataWash = pd.DataFrame.copy(final)
    dataWash = dataWash.drop(['activity','id'], axis = 1)
    predictionWash = modelWash.predict(dataWash)

    return 'drink:' + predictionDrink[0] + ' wash:' + predictionWash[0]


def get_features(sensor, data):
# sensor features per sensor and per axis
    sensor_val = data[['X','Y','Z']]
    sensor_features = {}
    for index, col in enumerate(sensor_val.columns):
        sensor_features[sensor + '_' + col + '_' + 'MIN'] = float(sensor_val[col].min())
        sensor_features[sensor + '_' + col + '_' + 'MAX'] = float(sensor_val[col].max())
        sensor_features[sensor + '_' + col + '_' + 'STD'] = float(sensor_val[col].std())
        sensor_features[sensor + '_' + col + '_' + 'MEA'] = float(sensor_val[col].mean())
        sensor_features[sensor + '_' + col + '_' + 'MED'] = float(sensor_val[col].median())
        sensor_features[sensor + '_' + col + '_' + 'IQR'] = float((sensor_val[col].quantile(.75) - sensor_val[col].quantile(.25)))
        sensor_features[sensor + '_' + col + '_' + 'ZCR'] = float(get_zero_crossings(sensor_val[col]))
    return sensor_features

def get_zero_crossings(values):
    b = 0
    if len(values) < 3:
        return b
    for a in (values):
        if (a > 0 and a + 1 <= 0):
            b = b + 1
        elif a <= 0 and a + 1 > 0:
            b = b + 1
    return b

def saveAsCSV(jsonDict):
    dt = datetime.datetime.now()

    username = jsonDict["Username"]
    if username is None:
        username = ""

    filename = jsonDict["WearableType"] + "-" + username + "-" + dt.strftime("%d-%m-%Y-%H-%M-%S")
    with open(filename + '.csv', mode='w', newline='') as ADLFile:
        ADLWriter = csv.writer(ADLFile, delimiter=';', quotechar='"', quoting=csv.QUOTE_MINIMAL)

        i = 0

        tsDict = jsonDict["GesturePointList"]
        tsFirst = tsDict[0]["TimeStamp"]
        tsLast = tsDict[-1]["TimeStamp"]
        tsDif = int(tsLast) - int(tsFirst)

        activity = jsonDict["Activity"]
        if activity is None:
            activity = ""

        ADLWriter.writerow([i, i, tsFirst, jsonDict["WearableType"], 'True', 0, tsDif, activity, username, jsonDict["WornWrist"]])

        for gesturePoint in jsonDict["GesturePointList"]:
            i = i + 1
            ADLWriter.writerow([i, i, gesturePoint["TimeStamp"], 'GA', gesturePoint["Accelerometer"]["X"], gesturePoint["Accelerometer"]["Y"], gesturePoint["Accelerometer"]["Z"]])
            i = i + 1
            ADLWriter.writerow([i, i, gesturePoint["TimeStamp"], 'GR', gesturePoint["Gyroscope"]["X"], gesturePoint["Gyroscope"]["Y"], gesturePoint["Gyroscope"]["Z"]])