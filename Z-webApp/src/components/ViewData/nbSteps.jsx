import React, { useState, useEffect } from 'react';
import './StepCounter.css';

const StepsChart = ({ steps }) => {
    const [stepsToday, setStepsToday] = useState(steps);
    const [stepsData, setStepsData] = useState([3000, 4000, 5000, 7000, 200, 0, 0]); // Example data for the week.

    // Update stepsData to include today's steps.
    useEffect(() => {
        const updatedStepsData = [...stepsData];
        updatedStepsData[new Date().getDay()] = steps;
        setStepsData(updatedStepsData);
        setStepsToday(steps);
    }, [steps]);

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
