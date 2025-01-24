/*import React, {useEffect, useState} from 'react';
import './main.css';

const CurrentUserData = () => {
    const [currentDate, setCurrentDate] = useState('');
    const [userName, setUserName] = useState('Utilisateur');
    console.log('CurrentUserData monté');

    useEffect(() => {
       // const date = new Date().toLocaleDateString();
        const now = new Date();
        const date = new Intl.DateTimeFormat('fr-FR', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
        }).format(now)
                
        setCurrentDate(date);
        //SImulation before wait database
        setUserName('Mouammar');
        
    }, []);
    return (
        <div className="current-user-data" style={{paddingTop :"90px"}}>
            <div className="date"> {currentDate}</div>
            <div className="username">{userName}</div>
        </div>
    );
}

export default CurrentUserData;*/
import React, { useEffect, useState } from 'react';
import './main.css';

const CurrentUserData = () => {
    const [currentDate, setCurrentDate] = useState('');
    const [userName, setUserName] = useState('Utilisateur');

    useEffect(() => {
        const now = new Date();
        const formattedDateTime = new Intl.DateTimeFormat('fr-FR', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
        }).format(now);

        setCurrentDate(formattedDateTime);
        setUserName('Mouammar'); // Simulation de connexion
    }, []);

    return (
        <div className="current-user-data" style={{ paddingTop: '90px' }}>
            {/* Section Date avec l'icône */}
            <div className="date">
                <i className="fas fa-calendar-alt" style={{ marginRight: '8px', color: 'yellow' }}></i>
                {currentDate}
            </div>

            {/* Section Username avec l'icône */}
            <div className="username">
                <i className="fas fa-user" style={{ marginRight: '8px', color: 'yellow' }}></i>
                {userName}
            </div>
        </div>
    );
};

export default CurrentUserData;
