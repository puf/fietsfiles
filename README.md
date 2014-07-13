fietsfiles
==========

Experiments with GPX, TCX and similar files that are gathered from bicycle rides.

json.html
---
Loads a geojson file and two tcx.json files from a (hardcoded) gist and displays the map and racers on a canvas. Preview available <a href="https://rawgit.com/puf/fietsfiles/master/json.html">here</a>.

The geojson file was downloaded from http://overpass-turbo.eu/. I don't remember the exact boundingbox, but it must've been something like this: http://overpass-api.de/api/interpreter?data=%5Bout%3Ajson%5D%3Bway%5B%22highway%22%5D%2839%2E07594306563115%2C-77%2E51442432403564%2C39%2E10005948059707%2C-77%2E4607801437378%29%3B%28%2E_%3B%3E%3B%29%3Bout%20meta%20qt%3B. Most notable about this query is that it only requests highways, since I don't intend to draw anything else at the moment. I manually compacted the downloaded geojson file a bit (stuffing points onto single lines), so that it would come in under the 1MB for a gist file.

The tcx.json files were originally downloaded from a Bkool Ant+ thingie. They were then converted into json, using the LINQPad file present in this repo. I further massaged the file a bit, to ensure that the points and distances are in the JSON as numbers instead of strings. If there is an official format for TCX information in JSON, I'd love to hear btw.