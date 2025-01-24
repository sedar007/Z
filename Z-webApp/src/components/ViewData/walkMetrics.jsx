import React, { useState, useEffect } from 'react';
import './StepCounter.css';

const WalkChart = () => {
    const [distanceToday, setDistanceToday] = useState(0); // Distance du jour en km.
    const [distanceData, setDistanceData] = useState([]); // Distances pour la semaine.

    const stepsData = [3000, 4000, 5000, 7000, 200, 0, 0]; // Exemple de données des pas pour la semaine.

    useEffect(() => {
        // Convertir les pas en kilomètres pour chaque jour.
        const distances = stepsData.map(steps => (steps * 0.0008).toFixed(2)); // Conversion et arrondi à 2 décimales.
        setDistanceData(distances);

        // Obtenir la distance pour le jour actuel.
        const todayDistance = distances[new Date().getDay()];
        setDistanceToday(todayDistance);
    }, [stepsData]);

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
                            height: `${(distance / 8) * 100}%`, // Hauteur relative au max de 8 km.
                            backgroundColor: index === new Date().getDay() ? '#005bb5' : '#2C35AA', // Couleur différente pour le jour actuel.
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
