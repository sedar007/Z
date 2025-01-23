import { API_URL } from '../../config';

const USER_API_URL = `${API_URL}user/create`;

async function _createUser(data) {
    console.log("business");
    console.log(data);
    try {
        const res = await fetch(`${USER_API_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                username: data.username,
                age: data.age,
                height: data.taille,
                weight: data.poids,
                password: data.password
                
            })
        });

        if (!res.ok) {
            throw new Error(`Erreur lors de la création de l'utilisateur`);
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
