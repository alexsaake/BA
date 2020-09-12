from flask import Flask, request
from validation import get_prediction, saveAsCSV

app = Flask(__name__)

@app.route('/api/adl/save/', methods=['PUT'])
def saveADL():
    if not request.json:
        abort(400)

    jsonDict = request.json
    
    saveAsCSV(jsonDict)

    return "OK", 200

@app.route('/api/adl/predict/', methods=['POST'])
def predictADL():
    if not request.json:
        abort(400)
        
    jsonDict = request.json

    prediction = get_prediction(jsonDict)

    return prediction, 200

if __name__ == "__main__":
    app.run(host='0.0.0.0', port='55000', debug=True)