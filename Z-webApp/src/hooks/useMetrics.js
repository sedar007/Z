import { useState, useEffect } from "react";
import axios from "axios";

const useMetrics = (apiUrl, token) => {
    const [metrics, setMetrics] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMetrics = async () => {
            try {
                setLoading(true);
                const response = await axios.get(apiUrl, {
                    headers: {
                        Authorization: `Bearer ${token}`, // Si un token d'authentification est requis
                    },
                });

                // On suppose que les métriques sont dans le body de la réponse JSON
                const { bmi, nbSteps, nbDistance } = response.data;

                setMetrics({ bmi, nbSteps, nbDistance });
            } catch (err) {
                setError(err.message || "Une erreur s'est produite");
            } finally {
                setLoading(false);
            }
        };

        fetchMetrics();
    }, [apiUrl, token]);

    return { metrics, loading, error };
};

export default useMetrics;
