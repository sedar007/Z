import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const HomePage = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token-z');
        console.log(token);
        if (token) {
            navigate('/view-indicators');
        } else {
            navigate('/login');
        }
    }, []); // Empty dependency array to run only once

    return (
        <div>
            <h1>Redirection en cours...</h1>
        </div>
    );
};

export default HomePage;
