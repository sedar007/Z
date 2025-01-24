import { useState, useEffect } from "react";
import { API_URL } from '../../config';

const useMetrics = (token, userId) => {
    const [metrics, setMetrics] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMetrics = async () => {
            try {
                setLoading(true);
                const response = await fetch(`${API_URL}metrics/getMetric/today/byAuthId/${userId}?unit=km`, {
                    headers: {
                        Authorization: `Bearer ${token}`, // If an authentication token is required
                    },
                });



                if (!response.ok) {
                    throw new Error('Something went wrong');
                }

                const data = await response.json();
                

                // Assuming the metrics are in the JSON response body
                //const { bmi, steps, distance, categoryImc, date } = data;

                setMetrics(data);
                console.log("eeee")
                console.log(data)
            } catch (err) {
                setError(err.message || "An error occurred");
            } finally {
                setLoading(false);
            }
        };

        fetchMetrics();
    }, [token, userId]);

    return { metrics, loading, error };
};

export default useMetrics;
