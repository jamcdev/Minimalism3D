// Entry point for whole application.

var express = require("express");

var app = express();

app.get("/", (req, res) => {
    res.send("Hello World!");
});

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

app.post("/test/:id", (req, res) => {
    // console.log(req.body);
    if (req.method == 'POST') {
        var jsonString = '';

        req.on('data', function (data) {
            jsonString += data;
        });

        req.on('end', function () {
            jsonString_parsed = JSON.parse(jsonString)
            console.log(jsonString_parsed);
            console.log((jsonString_parsed["test"]));
        });
    }
});

app.listen(8000, () => {
    console.log("Server started.");
});