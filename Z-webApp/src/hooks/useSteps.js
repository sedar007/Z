import { useState, useEffect } from 'react';

const useFetchSteps = (userId, token) => {
    const [steps, setSteps] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchSteps = async () => {
            try {
                setLoading(true);
                setError(null);

                // URL de l'API Swagger, remplacer par votre URL
                const url = `https://api.example.com/users/${userId}/steps?lastDays=7`;

                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${token}`, // En-tête pour le token d'authentification
                        'Content-Type': 'application/json', // En-tête pour JSON
                    },
                });

                if (!response.ok) {
                    throw new Error(`Erreur: ${response.status} ${response.statusText}`);
                }

                const data = await response.json();
                setSteps(data); // Supposons que `data` contient les étapes des 7 derniers jours
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        if (userId && token) {
            fetchSteps();
        }
    }, [userId, token]);

    return { steps, loading, error };
};

export default useFetchSteps;
