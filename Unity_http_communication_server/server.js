var http = require('http');
var express = require('express');
var bodyParser = require('body-parser');
var fs = require('fs');
var xml = require('xml');
var router = express.Router();


var app = express();
app.use(bodyParser.text({ type: 'text/xml' }))

//400 response
function send404Response(response){
    response.writeHead(404, {"Content-Type": "text/plain"});
    response.write("Error 404: Page not found!");
    response.end();
}

var feedbackXml = "";

app.get('/parameter', function (request, response){
    console.log("&&&&&&&&&&&&&&&&&&&& PARAM &&&&&&&&&&&&&&&&&&&&&&&&& ");
    response.writeHead(202, {"Content-Type": "text/xml"});
    fs.createReadStream("./parameter.xml").pipe(response)
});

app.get('/feedback', function (request, response){
    console.log("&&&&&&&&&&&&&&&&&&&& FEEDBACK &&&&&&&&&&&&&&&&&&&&&&&&& ");
    response.header("Content-Type", "text/xml");
    response.send(feedbackXml);
});

app.post('/', function (request, response){
    //response.header("Content-Type", "text/xml");
    console.log("[200] " + request.method + " to " + request.url);
    console.log(request.body);
    feedbackXml = request.body;
    response.send();
});
//handle a user request
/*function onRequest(request, response){
    if(request.method == 'GET'){
        if(request.url == '/param'){
        response.writeHead(202, {"Content-Type": "text/html"});
        fs.createReadStream("./param.html").pipe(response);
        }
        //if(request.url == '/feedback'){
            //response.writeHead(202, {"Content-Type": "text/xml"});
            //console.log("&&&&&&&&&&&&&&&&&&&& XML &&&&&&&&&&&&&&&&&&&&&&&&& ");
            //response.send(xml(feedbackXml));
        //}
    }else if(request.method == 'POST'){
        response.writeHead(202, {"Content-Type": "text/xml"});
        console.log("[200] " + request.method + " to " + request.url);

        request.on('data', function(chunk) {
            feedbackXml += chunk;
        });

        request.on('end', function() {
            response.writeHead(200, "OK", {'Content-Type': 'text/xml'});
            console.log(feedbackXml);
            response.end();
        })
    }
    else{
        send404Response(response);
    }
}
*/
http.createServer(app).listen(8888);
console.log("server running...");

module.exports = app;