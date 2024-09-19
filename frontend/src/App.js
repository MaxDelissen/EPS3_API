import React, { useState } from "react";
import logo from './logo.svg';
import './App.css';
import {fetchWeather} from "./WeatherFetcher";

function App() {
    const [temperatureC, setTemperatureC] = useState(null);
    const [summary, setSummary] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const [fetchedTime, setFetchedTime] = useState(null);

    const fetchData = async () => {
      setLoading(true);
      try{
          const weather = await fetchWeather();
            setTemperatureC(weather[0].temperatureC);
            setSummary(weather[0].summary);
            setError(null);
            setFetchedTime(new Date().toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit'}));
        } catch (error) {
            setError(error.message);
            setTemperatureC(null);
            setSummary(null);
        }
        setLoading(false);
    };

  return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />

          <h3>Enter your name below, and click the button for a personalized weather forecast!</h3>
            <input type="text" placeholder="Your name" required></input>
          <button onClick={fetchData} disabled={loading}> {/* Disable button while loading */}
            {loading ? "Loading..." : "Click me"}
          </button>

          {/* Display weather or error */}
          {error && <p style={{ color: 'red' }}>{error}</p>}

          {temperatureC !== null && summary !== null && (
              <h4>The Weather, fetched at {fetchedTime}, is {summary} with a temperature of {temperatureC}°C.</h4>
            )}
        </header>
      </div>
    );
}

export default App;
