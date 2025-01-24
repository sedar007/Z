import React from 'react';
import Activity from "./activity.jsx";
import Miles from "./nbSteps.jsx";
import WalkChart from "./walkMetrics.jsx";
import IMCVisualizer from './IMCVisualizer';
import useMetrics from '../../hooks/useMetrics';
import "./viewIndicators.css";

const ViewIndicators = () => {
    const token = localStorage.getItem('token-z'); // Retrieve the token from localStorage
    const userId = localStorage.getItem('idUser'); // Retrieve the user ID from localStorage

    const { metrics, loading, error } = useMetrics(token, userId);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!metrics) {
        return <div>No metrics available</div>;
    }

    console.log("metrics")
    console.log(metrics.steps)

    return (
        <div>
            <div className="view-indicators">
                <Activity />
                <Miles steps={metrics.steps} />
                <WalkChart distance={metrics.distance} />
                <IMCVisualizer weight={metrics.bmi} height={metrics.height}
                               categoryImc={metrics.categoryImc} />
            </div>
        </div>
    );
}

export default ViewIndicators;
