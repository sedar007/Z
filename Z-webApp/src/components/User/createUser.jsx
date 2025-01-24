import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './createUser.css'

function CreateUser() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        name: '',
        email: '',
        password: '',
        confirmPassword: '',
        termsAccepted: false
    });

    // Gérer les changements des champs
    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value
        });
    };

    // Gérer la soumission du formulaire
    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Form data submitted:', formData);

        if (formData.password !== formData.confirmPassword) {
            alert("Les mots de passe ne correspondent pas !");
            return;
        }

        if (!formData.termsAccepted) {
            alert("Vous devez accepter les conditions d'utilisation.");
            return;
        }
        navigate('/infos');
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '60vh', background: '',color:'white' }}>
            <div style={{ maxWidth: '500px', width: '100vh', padding: '20px', background: '', borderRadius: '8px', boxShadow: '0 0 10px rgba(0,0,0,0.1)' }}>
                <h2 style={{ textAlign: 'center', marginBottom: '20px',color:'white' }}>Créer un compte</h2>

                <form onSubmit={handleSubmit} >
                    <div style={{ marginBottom: '15px' }}>
                        <label>User Name</label>
                        <input
                            type="text"
                            className="form-control"
                            name="name"
                            value={formData.name}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>Email</label>
                        <input
                            type="email"
                            className="form-control"
                            name="email"
                            value={formData.email}
                            onChange={handleInputChange}
                            required
                            style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
                        />
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
                    </div>

                    <div style={{ marginBottom: '20px' }}>
                        <input
                            type="checkbox"
                            name="termsAccepted"
                            checked={formData.termsAccepted}
                            onChange={handleInputChange}
                        />
                        <label style={{ marginLeft: '10px' }}>J'accepte les conditions d'utilisation</label>
                    </div>

                    <button type="submit" style={{ width: '100%', padding: '10px', borderRadius: '5px', backgroundColor: '#2C35AA', color: '#fff', border: 'none', cursor: 'pointer' }}>
                        S'inscrire
                    </button>
                </form>
            </div>
        </div>
    );
}

export default CreateUser;
