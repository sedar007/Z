import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '@fortawesome/fontawesome-free/css/all.min.css';
import '../../pages/Login/css/bootstrap.min.css';  // Import Bootstrap
import '../../pages/Login/css/style.css';
import '../../pages/Login/scss/style.scss';
import postLogin from '../../business/postLogin.js';

const AuthPage = () => {
    const navigate = useNavigate();
    const [credentials, setCredentials] = useState({ username: '', password: '' });
    const [error, setError] = useState('');
    const [showPassword, setShowPassword] = useState(false);  // Ajout de l'état pour afficher/masquer le mot de passe


    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            navigate('/info');
        }
    }, [navigate]);


    const handleInputChange = (e) => {
        setCredentials({ ...credentials, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const us = await postLogin(credentials.username, credentials.password);
            connected(us);

        } catch (e) {
            setError('Nom d\'utilisateur ou mot de passe incorrect');
        }
    };

    const connected = (response) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('username', response.username);
        localStorage.setItem('idUser', response.id);
        setError('');
        navigate('/info');
    }

    return (
        <div className="img js-fullheight" style={{ minWidth: '150vh' }}>
            <section className="ftco-section">
                <div className="container">
                    <div className="row justify-content-center"></div>
                    <div className="row justify-content-center">
                        <div className="col-md-6 col-lg-4">
                            <div className="login-wrap p-0">
                                <h3 className="mb-4 text-center">Have an account?</h3>
                                {error && <p className="text-danger text-center">{error}</p>}
                                <form onSubmit={handleSubmit} className="signin-form">
                                    <div className="form-group">
                                        <input
                                            type="text"
                                            name="username"
                                            className="form-control"
                                            placeholder="Username"
                                            required
                                            value={credentials.username}
                                            onChange={handleInputChange}
                                        />
                                    </div>
                                    <div className="form-group position-relative">
                                        <input
                                            type={showPassword ? "text" : "password"} // Changer le type de l'input selon l'état
                                            name="password"
                                            className="form-control"
                                            placeholder="Password"
                                            required
                                            value={credentials.password}
                                            onChange={handleInputChange}
                                        />
                                        <span
                                            className={`fa fa-fw ${showPassword ? "fa-eye-slash" : "fa-eye"} field-icon`} // Alterner les icônes
                                            style={{
                                                position: 'absolute',
                                                right: '15px',
                                                top: '50%',
                                                transform: 'translateY(-50%)',
                                                cursor: 'pointer'
                                            }}
                                            onClick={() => setShowPassword(!showPassword)} // Toggle l'état du mot de passe
                                        ></span>
                                    </div>
                                    <div className="form-group">
                                        <button type="submit" className="form-control  btn-primary submit px-3" style={{backgroundColor:'#2C35AA',color:'white',fontWeight:'bold'}}>
                                            Sign In
                                        </button>
                                    </div>
                                    <div className="form-group d-md-flex">
                                        {/*<div className="w-50">
                                            <label className="checkbox-wrap checkbox-primary">
                                                Remember Me
                                                <input type="checkbox" defaultChecked />
                                                <span className="checkmark"></span>
                                            </label>
                                        </div>
                                        <div className="w-50 text-md-right">
                                            <a href="#" style={{ color: '#fff' }}>Forgot Password</a>
                                        </div>*/}
                                    </div>
                                </form>
                                <p className="w-100 text-center">&mdash; Or Sign In With &mdash;</p>
                                <div className="social d-flex text-center" >
                                    <a href="/create-account" className="px-2 py-2 ml-md-1 rounded" style={{backgroundColor:'#2C35AA',color:'white',fontWeight:'bold'}}>
                                        <span className="fa fa-twitter mr-2"></span> Create Account
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
};

export default AuthPage;
