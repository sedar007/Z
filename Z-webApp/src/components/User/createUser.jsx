import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { createUser } from "../../business/users.js";

import './createUser.css';
import useUsersSetter from "../../hooks/useUsersSetter.js";

function CreateUser() {
    const navigate = useNavigate();

    const [error, setError] = useState(null);
    const [formData, setFormData] = useState({
        username: '',
        age: '',
        poids: '',
        taille: '',
        password: '',
        confirmPassword: ''
    });
    const [errors, setErrors] = useState({});

    // Gérer les changements des champs
    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value
        });
    };

    const handleCreateUser = async (e) => {
        e.preventDefault();
        try {
            console.log('Form data submitted:', formData);

            let formErrors = {};

            console.log('Form data submitted:', formData);
            console.log(formData);
            const create = await createUser(formData);
            navigate('/view-indicators');
        } catch (e) {
            console.log(e);
            setError(e.message);
        }
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '60vh', background: '', color: 'white' }}>
            <div style={{ maxWidth: '500px', width: '100vh', padding: '20px', background: '', borderRadius: '8px', boxShadow: '0 0 10px rgba(0,0,0,0.1)' }}>
                <h2 style={{ textAlign: 'center', marginBottom: '20px', color: 'white' }}>Créer un compte</h2>

                <form>
                    <div style={{ marginBottom: '15px' }}>
                        <label>Nom d'utilisateur</label>
                        <input
                            type="text"
                            className="form-control"
                            name="username"
                            value={formData.username}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                        {errors.username && <p style={{ color: 'red' }}>{errors.username}</p>}
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>Âge</label>
                        <input
                            type="number"
                            className="form-control"
                            name="age"
                            value={formData.age}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                        {errors.age && <p style={{ color: 'red' }}>{errors.age}</p>}
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>Poids</label>
                        <input
                            type="number"
                            className="form-control"
                            name="poids"
                            value={formData.poids}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                        {errors.poids && <p style={{ color: 'red' }}>{errors.poids}</p>}
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>Taille</label>
                        <input
                            type="number"
                            className="form-control"
                            name="taille"
                            value={formData.taille}
                            onChange={handleInputChange}
                            required
                            step="0.01"
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                        {errors.taille && <p style={{ color: 'red' }}>{errors.taille}</p>}
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>Mot de passe</label>
                        <input
                            type="password"
                            className="form-control"
                            name="password"
                            value={formData.password}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                        {errors.password && <p style={{ color: 'red' }}>{errors.password}</p>}
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>Confirmer le mot de passe</label>
                        <input
                            type="password"
                            className="form-control"
                            name="confirmPassword"
                            value={formData.confirmPassword}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                        {errors.confirmPassword && <p style={{ color: 'red' }}>{errors.confirmPassword}</p>}
                    </div>

                    <button type="button" style={{
                        width: '100%',
                        padding: '10px',
                        borderRadius: '5px',
                        backgroundColor: '#2C35AA',
                        color: '#fff',
                        border: 'none',
                        cursor: 'pointer'
                    }} onClick={handleCreateUser}>
                        S'inscrire
                    </button>

                    {error && <p style={{ color: 'red', marginTop: '15px' }}>{error}</p>}
                </form>
            </div>
        </div>
    );
}

export default CreateUser;
