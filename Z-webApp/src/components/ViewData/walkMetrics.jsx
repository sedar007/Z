import React, { useState, useEffect } from 'react';
import './StepCounter.css';

const WalkChart = ({ distance }) => {
    const [distanceToday, setDistanceToday] = useState(distance);
    const [distanceData, setDistanceData] = useState([2.4, 3.2, 4.0, 5.6, 0.16, 0, 0]); // Example data for the week.

    // Update distanceData to include today's distance.
    useEffect(() => {
        const updatedDistanceData = [...distanceData];
        updatedDistanceData[new Date().getDay()] = distance;
        setDistanceData(updatedDistanceData);
        setDistanceToday(distance);
    }, [distance]);

    return (
        <div className="container-steps">
            <h1>Distance parcourue aujourd'hui</h1>
            <p className="steps-count">{distanceToday} km</p>
            <h2>Graphique des distances cette semaine</h2>
            <div className="chart">
                {distanceData.map((distance, index) => (
                    <div
                        key={index}
                        className="bar"
                        style={{
                            height: `${(distance / 8) * 100}%`, // Height relative to max (8 km).
                            backgroundColor: index === new Date().getDay() ? '#005bb5' : '#2C35AA', // Different color for the current day.
                        }}
                    >
                        <span>{distance} km</span>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default WalkChart;
