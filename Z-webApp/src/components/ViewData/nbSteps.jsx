import React, { useState, useEffect } from 'react';
import './StepCounter.css';

const StepsChart = ({ steps, stepsData }) => {
    const [stepsToday, setStepsToday] = useState(steps);

    // Update stepsToday to include today's steps.
    useEffect(() => {
        if (stepsData.length > 0) {
            const todaySteps = stepsData[new Date().getDay()]; // Get today's steps
            setStepsToday(todaySteps);
        }
    }, [stepsData]);

    return (
        <div className="container-steps">
            <h1>Nombre de pas aujourd'hui</h1>
            <p className="steps-count">{stepsToday}</p>
            <h2>Graphique des pas cette semaine</h2>
            <div className="chart">
                {stepsData.map((steps, index) => (
                    <div
                        key={index}
                        className="bar"
                        style={{
                            height: `${(steps / 10000) * 100}%`, // Height relative to max (10,000).
                            backgroundColor: index === new Date().getDay() ? '#2C35AA' : '#2C35AA', // Different color for the current day.
                        }}
                    >
                        <span>{steps}</span>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default StepsChart;
