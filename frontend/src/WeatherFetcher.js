export async function fetchWeather() {
    const response = await fetch('http://localhost:5192/WeatherForecast'); //Explanation: This is the URL of the API that we want to fetch data from, in this step we start fetching the data.
    const data = await response.json(); //Explanation: after all the data is fetched (therefore the await keyword), we convert the data to JSON format.
    if(response.ok) {
        return data; //Explanation: if the response is okay, we return the data.
    }
    else {
        throw new Error("Network response was not ok."); //Explanation: if the response is not okay, we throw an error.
    }
}