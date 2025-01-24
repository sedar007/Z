import { useState, useEffect } from 'react';

const useFetchDistance = (userId, token) => {
    const [distance, setDistance] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchDistance = async () => {
            try {
                setLoading(true);
                setError(null);

                // URL de l'API Swagger, remplacer par votre URL
                const url = `https://api.example.com/users/${userId}/distance?lastDays=7`;

                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${token}`, // En-tête pour le token d'authentification
                        'Content-Type': 'application/json', // Spécifiez si nécessaire
                    },
                });

                if (!response.ok) {
                    throw new Error(`Erreur: ${response.status} ${response.statusText}`);
                }

                const data = await response.json();
                setDistance(data); // Supposons que `data` contient les étapes des 7 derniers jours
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        if (userId && token) {
            fetchDistance();
        }
    }, [userId, token]);

    return { distance, loading, error };
};

export default useFetchDistance;
