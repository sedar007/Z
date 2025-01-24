import Footer from '../Footer';
import Header from '../Header';
import { Outlet } from 'react-router-dom';
import CurrentUserData from "../Currentdata/index.jsx";

export default function Layout() {
	return (
		<>
			{/* Header fixe */}
			<Header />

			{/* Ajout de CurrentUserData dans le Body */}
			<div className="layout-container">
				<CurrentUserData /> {/* Date et nom d'utilisateur */}
				<main className="main-content">
					<Outlet />
				</main>
			</div>

			{/* Footer fixe */}
			<Footer />
		</>
	);
}