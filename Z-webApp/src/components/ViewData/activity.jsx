import React from 'react';
import { Line } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import "./activity.css";

// Enregistrement des composants nécessaires pour Chart.js
ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
);

const HeartRateChart = () => {
    // Données fictives pour tester le graphique
    const mockHeartRateData = [
        { date: '2025-01-17', heartRate: 72 },
        { date: '2025-01-18', heartRate: 78 },
        { date: '2025-01-19', heartRate: 75 },
        { date: '2025-01-20', heartRate: 80 },
        { date: '2025-01-21', heartRate: 77 },
        { date: '2025-01-22', heartRate: 74 },
        { date: '2025-01-23', heartRate: 76 },
    ];

    // Préparation des données pour le graphique
    const chartData = {
        labels: mockHeartRateData.map((entry) => entry.date), // Les dates
        datasets: [
            {
                label: 'Fréquence cardiaque (bpm)',
                data: mockHeartRateData.map((entry) => entry.heartRate), // Les valeurs de fréquence cardiaque
                fill: false,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: '#2C35AA',
                tension: 0.3, // Adoucissement de la courbe
            },
        ],
    };

    // Options pour personnaliser le graphique
    const chartOptions = {
        responsive: true,
        plugins: {
            legend: {
                display: true,
                position: 'top',
            },
        },
        scales: {
            x: {
                type: 'category', // Important : Spécifiez le type de l'axe X
                title: {
                    display: true,
                    text: 'Date',
                },
            },
            y: {
                title: {
                    display: true,
                    text: 'BPM (Battements par minute)',
                },
                beginAtZero: false,
            },
        },
    };

    return (
        <div className="activity-container">
            <h2>Fréquence cardiaque des 7 derniers jours</h2>
            <Line data={chartData} options={chartOptions} />
        </div>
    );
};

export default HeartRateChart;
