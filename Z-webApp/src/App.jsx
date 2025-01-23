
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css'
import HomePage from "./pages/Home";
import LoginPage from "./pages/Login";
import InfoPage from './pages/Infos';
import CreatePage from './pages/CreateUser/createUser.jsx';
import Header from './components/Header';


function App() {


  return (

      <Router>
          <Header />
          <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/create-account" element={<CreatePage />} />
              <Route path="/infos" element={<InfoPage />} />
          </Routes>
      </Router>

  )
}

export default App