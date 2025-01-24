import React from "react";
import "./activity.css";

// eslint-disable-next-line react/prop-types
const ActivityRing = ({ current, goal }) => {
    const progress = (current / goal) * 100;

    return (
        <div className="activity-container">
            <h2>Anneau Activité</h2>
            <div className="ring">
                <svg width="200" height="200" viewBox="0 0 36 36">
                    <circle cx="18" cy="18" r="16" className="background-ring"></circle>
                    <circle
                        cx="18"
                        cy="18"
                        r="16"
                        className="progress-ring"
                        style={{ strokeDasharray: `${progress}, 100` }}
                    ></circle>
                </svg>
                <div className="text">{current}/{goal}</div>
            </div>
            <p className="kcal">{current} / {goal} KCAL</p>

        </div>
    );
};

export default ActivityRing;
