import { API_URL } from '../../config';

const AUTH_API_URL = `${API_URL}admin/Auth/login`;

export default async function postLogin(username, password) {
    const res = await fetch(AUTH_API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            username: username,
            password: password
        })
    });

    if (res.status !== 200) {
        throw new Error('Erreur lors de l\'auth');
    }
    const response = await res.json();

    console.log(response);

    return response;
}
