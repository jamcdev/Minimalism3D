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

// Link Schema to collection.

var ItemPost = mongoose.model('ItemPost', ItemPostSchema);

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

// Start server.

app.listen(port || 8000, () => {
    console.log("Server Database started.");
});