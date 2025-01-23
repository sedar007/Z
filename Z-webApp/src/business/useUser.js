import {useEffect, useState} from "react";
import { API_URL } from '../../config';

const USER_API_URL = `${API_URL}user/getUser`;


async function getUser(id){
    const res = await fetch(`${USER_API_URL}/${id}`);

    if(res.ok)
        return await res.json();

    throw new Error('Failed to fetch user');
}


export function useUser(id){
    const [user, setUser] = useState(null);

    useEffect(() => {
        if(!id) return;
        async function getData(){
            setUser(await getUser(id));
        }
        getData();
    }, [id]);

    return user;
}
