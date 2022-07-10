// Entry point for whole application.

// Setup Express

var express = require("express");
var app = express();
var port = process.env.PORT || 8000;

// Setup Mongoose

require('dotenv').config();

var mongoose = require("mongoose");
mongoose.connect(process.env.MONGODB_URI || "mongodb://localhost/testdb", { useNewUrlParser: true });
var Schema = mongoose.Schema;

// Setup MongoClient to get documents from DB.

var MongoClient = require('mongodb').MongoClient;
var url = "mongodb+srv://admin:passpass@cluster0.za92g.mongodb.net/test";

// Setup Post Schema

var ItemPostSchema = new Schema({
    itemPhoto: String,
    itemNickName: String,
    itemPhoneNumber: String,
    itemTimeForPickup: String,
    itemX: Number,
    itemY: Number,
    itemStation: String,
    itemTimePosted: String,
})

var UserDataSchema = new Schema({
    deviceID: String,
    partnerID: String,
    committed: Boolean,
    karma: Number,
    previousPunishment: Number,
    phoneNumber: Number,
    itemID: String,
    tokens: Number,
})

// Link Schema to collection.

var ItemPost = mongoose.model('ItemPost', ItemPostSchema);
var UserData = mongoose.model('UserData', UserDataSchema);

// Homepage

app.get("/", (req, res) => {
    res.send("Hello World! Server with Database.");
});

// Client can get data from here.

app.get("/user/:id", (req, res) => {
    var dummyData = {
        userID: req.params["id"],
        username: "user1",
        wins: 18,
        losses: 1000,
        someArray: [
            { name: "foo", value: 2.5 },
            { name: "bar", value: 3.0 },
        ]
    };

    res.send(dummyData);
});

// Client can post data to server.

app.post("/test/:id", (req, res) => {
    if (req.method == 'POST') {
        var jsonString = '';

        req.on('data', function (data) {
            jsonString += data;
        });

        req.on('end', function () {
            jsonString_parsed = JSON.parse(jsonString)
            console.log(jsonString_parsed);
            // console.log((jsonString_parsed["test"]));

            // Save Post to Database.
            ItemPost.create(
                {
                    itemPhoto: jsonString_parsed["itemPhotoJSON"],
                    itemNickName: jsonString_parsed["itemNickNameJSON"],
                    itemPhoneNumber: jsonString_parsed["itemPhoneNumberJSON"],
                    itemTimeForPickup: jsonString_parsed["itemTimeForPickupJSON"],
                    itemX: jsonString_parsed["itemXJSON"],
                    itemY: jsonString_parsed["itemYJSON"],
                    itemStation: jsonString_parsed["itemStationJSON"],
                    itemTimePosted: jsonString_parsed["itemTimePostedJSON"]
                },
                (error, itemPost) => {
                    console.log("Error was: " + error);
                    // console.log(itemPost);
                }
            )
        });
    }

});

// Get all documents from items

app.get("/items/:id", (req, res) => {

    MongoClient.connect(url, function (err, db) {
        if (err)
            throw error;
        var dbo = db.db("test");
        dbo.collection("itemposts").find({}).toArray(function (err, result) {
            if (err)
                throw err;
            console.log(result);
            res.send(result);
            db.close();
        });
    });

    // res.send(items);
});

// Send NEW user device ID to database

app.post("/userData/:id", (req, res) => {
    if (req.method == 'POST') {
        var jsonString = '';

        req.on('data', function (data) {
            jsonString += data;
        });

        req.on('end', function () {
            // jsonString_parsed = JSON.parse(jsonString)
            // console.log(jsonString_parsed);
            // console.log((jsonString_parsed["test"]));
            console.log(jsonString); // has ID plaintext

            // Save Post to Database.
            UserData.create(
                {
                    deviceID: jsonString,
                    partnerID: "",
                    committed: false,
                    karma: 1,
                    previousPunishment: 1,
                    phoneNumber: 0,
                    itemID: "",
                    tokens: 1,
                },
                (error, itemPost) => {
                    console.log("Error was: " + error);
                    console.log(itemPost);
                }
            )

        });
    }

});

// Get existing user details

app.get("/userData/:deviceID", (req, res) => {
    var deviceID = req.params["deviceID"];
    
    MongoClient.connect(url, function (err, db) {
        if (err)
            throw error;
        var dbo = db.db("test");
        var query = {"deviceID":deviceID};
        dbo.collection("userdatas").find(query).toArray(function (err, result) {
            if (err)
                throw err;
            console.log(result);
            res.send(result);
            db.close();
        });
    });

    // res.send(items);
});

// Update server with new tokens.
app.post("/updateTokens/:id", (req, res) => {
    if (req.method == 'POST') {
        var jsonString = '';

        req.on('data', function (data) {
            jsonString += data;
        });

        req.on('end', function () {
            jsonString_parsed = JSON.parse(jsonString)
            console.log(jsonString_parsed);
            

            // Update token based on DeviceIDJSON, tokensJSON.
            MongoClient.connect(url, function(err, db) {
                if (err) throw err;
                var dbo = db.db("test");
                console.log(jsonString_parsed["DeviceIDJSON"]);
                console.log(jsonString_parsed["tokensJSON"]);
                var myquery = { deviceID: jsonString_parsed["DeviceIDJSON"] };
                var newvalues = { $set: {tokens: jsonString_parsed["tokensJSON"] } };
                dbo.collection("userdatas").updateOne(myquery, newvalues, function(err, res) {
                  if (err) throw err;
                  console.log("1 document updated");
                  db.close();
                });
              });

        });
    }

});

// Start server.

app.listen(port || 8000, () => {
    console.log("Server Database started.");
});