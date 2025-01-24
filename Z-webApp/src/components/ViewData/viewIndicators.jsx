import React, { useState, useEffect } from 'react';
import Activity from "./activity.jsx"
import Miles from "./nbSteps.jsx"
import WalkChart from "./walkMetrics.jsx";
import IMCVisualizer from './IMCVisualizer';

import "./viewIndicators.css"


const ViewIndicators = () => {

    return (
        <div>
            <div className="view-indicators">
                <Activity  />
                <Miles  />
                <WalkChart/>
                <IMCVisualizer weight={90} height={1.8} />
            </div>
            {/*<div>
                <Miles gaugeValue={30} label="Distance Parcourue" label2="Marcher" unit="Km" objectif={10000}/>
            </div>*/}
        </div>

    );
}

export default ViewIndicators;