import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const HomePage = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const userSession = localStorage.getItem('user');  // Vérifie si la session existe

        if (userSession) {
            navigate('/info');  // Redirige vers la page info si la session existe
        } else {
            navigate('/login');  // Sinon redirige vers la page d'authentification
        }
    }, [navigate]);

    return (
        <div>
            <h1>Redirection en cours...</h1>
        </div>
    );
};

export default HomePage;
