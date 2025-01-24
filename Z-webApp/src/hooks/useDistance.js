import { useState, useEffect } from 'react';
import { API_URL } from '../../config';

const useFetchDistance = (userId, token) => {
    const [distance, setDistance] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchDistance = async () => {
            try {
                setLoading(true);
                setError(null);

                const response = await fetch(`${API_URL}user/getLast7DaysDistances/${userId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`, // Authentication token header
                        'Content-Type': 'application/json', // JSON header
                    },
                });

                if (!response.ok) {
                    throw new Error('Something went wrong');
                }

                const data = await response.json();
                console.log("0000")
                console.log(data);
                const distanceData = data.distances.map(item => item.distances); // Extract distance from the response
                setDistance(distanceData);
            } catch (err) {
                setError(err.message || 'An error occurred');
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
