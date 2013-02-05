geckonet
========

A comprehensive C# API wrapper library for accessing Geckoboard.com, using XML or JSON to read/write widget data easily using strong-typed models.

## Usage

To see how the endpoints from the asp.net web api project work, do the following to enter the following information for the custom widget on your geckoboard.

1. Click "Add widget"
2. Select the category "Custom Widgets"
3. Select a widget, like "RAG Numbers"
4. Enter the following information:
* Method: Polling
* URL data feed: http://geckonet.azurewebsites.net/api/Values/<method name here>
* API key: Any valid guid, since the site uses guid-based authorization
* Widget type: Custom
* Feed Format: JSON
* Request Type: GET
* Reload Time: 15 Minutes
5. To finish, click "Add to Dashboard".

The widget should then appear on your dashboard with random data everytime it's refreshed.

#### Thanks!
Kori Francis ([@djbyter](http://twitter.com/djbyter))