import Footer from '../Footer';
import Header from '../Header';
import { Outlet } from 'react-router-dom';
import Home from '../../pages/Home';

export default function Layout() {
	return (
		<>
			<Header />
			< Outlet/>
			<Footer />
		</>
	);
}