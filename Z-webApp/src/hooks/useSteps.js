import { useState, useEffect } from 'react';
import { API_URL } from '../../config';

const useFetchSteps = (userId, token) => {
    const [steps, setSteps] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchSteps = async () => {
            try {
                setLoading(true);
                setError(null);
                const response = await fetch(`${API_URL}user/getLast7DaysSteps/${userId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`, // Authentication token header
                        'Content-Type': 'application/json', // JSON header
                    },
                });

                if (!response.ok) {
                    throw new Error('Something went wrong');
                }

                const data = await response.json();
                const stepsData = data.steps.map(item => item.steps);
                console.log("--");
                console.log(stepsData);
                setSteps(stepsData); // Assuming `data` contains the steps for the last 7 days
            } catch (err) {
                setError(err.message || 'An error occurred');
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
