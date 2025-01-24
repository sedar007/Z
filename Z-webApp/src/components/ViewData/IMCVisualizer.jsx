import React from 'react';
import './IMCVisualizer.css';

const IMCVisualizer = ({ weight, height }) => {
    // Calculer l'IMC en fonction des props.
    const calculateBMI = (weight, height) => {
        return (weight / (height * height)).toFixed(2);
    };

    const getBMICategory = (bmi) => {
        if (bmi < 18.5) return 'Maigreur';
        if (bmi >= 18.5 && bmi < 25) return 'Normal';
        if (bmi >= 25 && bmi < 30) return 'Surpoids';
        if (bmi >= 30) return 'Obésité';
        return '';
    };

    const bmi = calculateBMI(weight, height);
    const category = getBMICategory(bmi);

    return (
        <div className="imc-container">
            <h1>Visualisation de l'IMC</h1>
            <div className="result">
                <p>Poids : <strong>{weight} kg</strong></p>
                <p>Taille : <strong>{height} m</strong></p>
                <p>Votre IMC : <strong>{bmi}</strong></p>
                <p>Catégorie : <span className={`category ${category.toLowerCase()}`}>{category}</span></p>
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
