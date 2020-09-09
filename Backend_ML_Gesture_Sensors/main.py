import json
import csv
from flask import Flask, request
import datetime
from validation import get_prediction

app = Flask(__name__)

@app.route('/api/adl/save/', methods=['PUT'])
def saveADL():
    if not request.json:
        abort(400)

    jsonDict = request.json

    dt = datetime.datetime.now()

    username = jsonDict["Username"]
    if username is None:
        username = ""

    filename = jsonDict["WearableType"] + "-" + username + "-" + dt.strftime("%d-%m-%Y-%H-%M-%S")
    with open(filename + '.csv', mode='w', newline='') as ADLFile:
        ADLWriter = csv.writer(ADLFile, delimiter=';', quotechar='"', quoting=csv.QUOTE_MINIMAL)

        i = 0

        tsDict = jsonDict["GesturePointList"]
        tsFirst = tsDict[0]
        tsLast = tsDict[-1]

        activity = jsonDict["Activity"]
        if activity is None:
            activity = ""

        ADLWriter.writerow([i, i, tsFirst["TimeStamp"], jsonDict["WearableType"], 'True', 0, tsLast["TimeStamp"], activity, username, jsonDict["WornWrist"]])

        for gesturePoint in jsonDict["GesturePointList"]:
            i = i + 1
            ADLWriter.writerow([i, i, gesturePoint["TimeStamp"], 'GA', gesturePoint["Accelerometer"]["X"], gesturePoint["Accelerometer"]["Y"], gesturePoint["Accelerometer"]["Z"]])
            i = i + 1
            ADLWriter.writerow([i, i, gesturePoint["TimeStamp"], 'GR', gesturePoint["Gyroscope"]["X"], gesturePoint["Gyroscope"]["Y"], gesturePoint["Gyroscope"]["Z"]])

    return 200

@app.route('/api/adl/predict/', methods=['POST'])
def predictADL():
    if not request.json:
        abort(400)
        
    jsonDict = request.json

    prediction = get_prediction(jsonDict)

    return prediction, 200

if __name__ == "__main__":
    app.run(host='0.0.0.0', port='5000', debug=True)