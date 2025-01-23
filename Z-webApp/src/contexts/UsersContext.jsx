import {createContext, useEffect, useState} from 'react';
export const UsersContext = createContext();

export function UsersContextProvider({ children }){

   // const [village, setVillage ] = useState(parseInt(localStorage.village || '0'));




    const [users, setUsers ] = useState(null);

    useEffect(() => {
       //localStorage.setItem('users', users);
    }, [users])

    return (
        <UsersContext.Provider value={{users, setUsers}} >
            {children}
        </UsersContext.Provider>
    )
}