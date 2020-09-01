from flask import Flask
from flask_restful import Api, Resource
import json

app = Flask(__name__)
api = Api(app)

class BackendAPI(Resource):
    def get(self):
        return {"Hello": "Hello World"}, 200

api.add_resource(BackendAPI, "/api/")

if __name__ == "__main__":
    app.run(host='0.0.0.0', port='5000', debug=True)
