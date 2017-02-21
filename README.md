geckonet
========

A comprehensive C# API wrapper library for accessing Geckoboard.com, using XML or JSON to read/write widget data easily using strong-typed models using the documentation listed [here](https://developer-custom.geckoboard.com/#introduction).

## Usage

To see how the endpoints from the asp.net web api project work, do the following to enter the following information for the custom widget on your geckoboard.

1. Click "Add widget"
2. Select the category "Custom Widgets"
3. Select a widget, like "RAG Numbers"
4. Enter the following information:

	**Method:** Polling  
	**URL data feed:** http://geckonet.azurewebsites.net/api/Widgets/<method name here>  
	**API key:** A valid guid, since the site uses guid-based authorization. Should be 27c4168c-5b25-44ac-824a-e77a2984522c  
	**Widget type:** Custom  
	**Feed Format:** JSON  
	**Request Type:** GET  
	**Reload Time:** 15 Minutes  

5. To finish, click "Add to Dashboard".

The widget should then appear on your dashboard with random data everytime it's refreshed.

## Live Examples

### Live Widget Endpoints
* `http://geckonet.azurewebsites.net/api/widgets/numberandsecondarystat?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/widgets/ragnumbersonly?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/widgets/ragcolumnandnumbers?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/widgets/text?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`

### Live Chart Endpoints
* `http://geckonet.azurewebsites.net/api/charts/map?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/charts/line?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/charts/geckometer?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/charts/funnel?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/charts/bullet?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`
* `http://geckonet.azurewebsites.net/api/charts/highchart?apiKey=27c4168c-5b25-44ac-824a-e77a2984522c`

#### Thanks!
Kori Francis ([@djbyter](http://twitter.com/djbyter))
