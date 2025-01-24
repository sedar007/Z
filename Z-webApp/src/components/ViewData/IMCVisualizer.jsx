import React from 'react';
import './IMCVisualizer.css';

const IMCVisualizer = ({ weight, height, categoryImc }) => {
    // Calculer l'IMC en fonction des props.
    const calculateBMI = (weight, height) => {
        return (weight / (height * height)).toFixed(2);
    };
    

    const bmi = calculateBMI(weight, height);


    return (
        <div className="imc-container">
            <h1>Visualisation de l'IMC</h1>
            <div className="result">
                <p>Poids : <strong>{weight} kg</strong></p>
                <p>Taille : <strong>{height} m</strong></p>
                <p>Votre IMC : <strong>{bmi}</strong></p>
                <p>Catégorie : <span className={`categoryImc ${categoryImc.toLowerCase()}`}>{categoryImc}</span></p>
                <div className="bmi-bar">
                    <div
                        className="indicator"
                        style={{
                            left: `${Math.min((bmi / 40) * 100, 100)}%`, // Positionner l'indicateur sur une échelle de 0 à 40.
                        }}
                    ></div>
                </div>
                <div className="bmi-scale">
                    <span>Maigreur</span>
                    <span>Normal</span>
                    <span>Surpoids</span>
                    <span>Obésité</span>
                </div>
            </div>
        </div>
    );
};

export default IMCVisualizer;
