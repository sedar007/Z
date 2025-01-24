import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
const HealthIndicatorForm = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        fullName: '',
        age: '',
        weight: '',
        height: '',
        activityLevel: '',
        medicalHistory: '',
    });

    const [submitted, setSubmitted] = useState(false);

    const handleInputChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Données soumises:', formData);
        setSubmitted(true);
    };

    return (
        <div className="container" style={{ height: '80vh',width: '100vh',backgroundSize: 'cover' }} >
            <h2 className="text-center mb-4">Formulaire dindicateur de Santé</h2>
            {submitted ? (
                navigate("/view-indicators")
            ) : (
                <form onSubmit={handleSubmit} className="signin-form" style={ {minHeight: '90vh' }}>
                    <div className="form-group" >
                        <label style={{color:'white'}}>Nom complet</label>
                        <input
                            type="text"
                            name="fullName"
                            className="form-control"
                            placeholder="Entrez votre nom complet"
                            required
                            value={formData.fullName}
                            onChange={handleInputChange}
                        />
                    </div>

                    <div className="form-group mb-3">
                        <label style={{color:'white'}}>Âge</label>
                        <input
                            type="number"
                            name="age"
                            className="form-control"
                            placeholder="Entrez votre âge"
                            required
                            min="0"
                            value={formData.age}
                            onChange={handleInputChange}
                        />
                    </div>

                    <div className="form-group mb-3">
                        <label style={{color:'white'}}>Poids (kg)</label>
                        <input
                            type="number"
                            name="weight"
                            className="form-control"
                            placeholder="Entrez votre poids en kg"
                            required
                            min="0"
                            value={formData.weight}
                            onChange={handleInputChange}
                        />
                    </div>

                    <div className="form-group mb-3">
                        <label style={{color:'white'}}>Taille (cm)</label>
                        <input
                            type="number"
                            name="height"
                            className="form-control"
                            placeholder="Entrez votre taille en cm"
                            required
                            min="0"
                            value={formData.height}
                            onChange={handleInputChange}
                        />
                    </div>

                    <div className="form-group mb-3">
                        <label style={{color:'white'}}>Niveau d'activité physique</label>
                        <select
                            name="activityLevel"
                            className="form-control"
                            required
                            value={formData.activityLevel}
                            onChange={handleInputChange}
                        >
                            <option value="" style={{color:'black'}}>Sélectionnez un niveau</option>
                            <option value="sédentaire" style={{color:'black'}}>Sédentaire</option>
                            <option value="légèrement actif" style={{color:'black'}}>Légèrement actif</option>
                            <option value="modérément actif" style={{color:'black'}}>Modérément actif</option>
                            <option value="très actif" style={{color:'black'}}>Très actif</option>
                        </select>
                    </div>

                    <div className="form-group mb-3">
                        <label style={{color:'white'}}>Antécédents médicaux</label>
                        <textarea
                            name="medicalHistory"
                            className="form-control"
                            placeholder="Mentionnez vos antécédents médicaux (diabète, hypertension, etc.)"
                            rows="3"
                            value={formData.medicalHistory}
                            onChange={handleInputChange}
                        ></textarea>
                    </div>

                    <div className="form-group">
                        <button  className="btn-primary w-100" style={{backgroundColor:'#2C35AA',color:'white',fontWeight:'bold'}}>
                            Soumettre
                        </button>
                    </div>
                </form>
            )}
        </div>
    );
};

export default HealthIndicatorForm;
