import {useContext} from "react";
import { UsersContext} from "../contexts/UsersContext.jsx";

export default function useUsersSetter(){
    const { setUsers } = useContext(UsersContext) ;
    return setUsers;
}

