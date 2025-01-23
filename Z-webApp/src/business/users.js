import { API_URL } from '../../config';

const USER_API_URL = `${API_URL}user/create`;

async function _createUser(data) {
    console.log("business");
    console.log(data);
    console.log(USER_API_URL);
    try {
        const res = await fetch(`${USER_API_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                name: data.username,
                age: parseInt(data.age, 10),
                height: parseFloat(data.taille),
                weight: parseInt(data.poids, 10),
                password: data.password
            })
        });

        if (!res.ok) {
            const errorText = await res.text();
            throw new Error(errorText);
        }

        const responseText = await res.text();
        console.log('Response Text:', responseText);

        const jsObjet = JSON.parse(responseText);
        console.log(jsObjet);
        return jsObjet;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}



export async function createUser(datas) {
    return await _createUser(datas);
}
