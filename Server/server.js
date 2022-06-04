// Entry point for whole application.

var express = require("express");

var app = express();

app.get("/", (req, res) => {
    res.send("Hello World!");
});

app.get("/user/", (req,res)=>{
    var dummyData = {
        username: "user1",
        wins:18,
        losses:1000
    };

    res.send(dummyData);
});

app.listen(8000, ()=>{
    console.log("Server started.");
});