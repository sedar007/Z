import React, { useState, useEffect } from 'react';
import './StepCounter.css';

const StepsChart = () => {
    const [stepsToday, setStepsToday] = useState(0);
    const [stepsData, setStepsData] = useState([3000, 4000, 5000, 7000, 200, 0, 0]); // Exemple de données pour la semaine.

    // Simuler les pas d'aujourd'hui au chargement.
    useEffect(() => {
        const todaySteps = stepsData[new Date().getDay()]; // Obtenir les pas du jour actuel.
        setStepsToday(todaySteps);
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
                            height: `${(steps / 10000) * 100}%`, // Hauteur relative au max (10 000).
                            backgroundColor: index === new Date().getDay() ? '#2C35AA' : '#2C35AA', // Couleur différente pour le jour actuel.
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
