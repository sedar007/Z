import React, { useState, useEffect } from 'react';
import './StepCounter.css';

const WalkChart = ({ distance, distanceData }) => {
    const [distanceToday, setDistanceToday] = useState(distance);

    // Update distanceToday to include today's distance.
    useEffect(() => {
        if (distanceData.length > 0) {
            const todayDistance = distanceData[new Date().getDay()]; // Get today's distance
            setDistanceToday(todayDistance);
        }
    }, [distanceData]);

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
