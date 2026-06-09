import React, { useState, useEffect } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuthStore } from "../../store/useAuthStore";
import styles from './Sidebar.module.scss';
import SearchBar from "../SearchBar/SearchBar";

const Sidebar = ({ setUsers }) => {
  const [activeLink, setActiveLink] = useState("");
  const location = useLocation();
  const navigate = useNavigate();

  const { user, token } = useAuthStore();

  useEffect(() => {
    setActiveLink(location.pathname);
  }, [location]);

  const handleLinkClick = (path) => {
    setActiveLink(path);
    navigate(path);
  };

  const handleLogout = () => {
    useAuthStore.getState().logout();
    navigate("/login");
  };

  const handleSearchSubmit = (query) => {
    e.preventDefault();
    navigate(`/search?query=${encodeURIComponent(query)}`);
  };

  return (
    <div className={styles.container}>
      <nav>
        <ul className={styles.navListUpper}>
          <li className={`${styles.navItem} ${activeLink === "/" ? styles.active : ""}`}>
            <Link to="/" onClick={() => handleLinkClick("/")}><h2>InstaClone</h2></Link>
          </li>
          <SearchBar onSubmit={handleSearchSubmit} />
          <li className={`${styles.navItem} ${activeLink === "/account" ? styles.active : ""}`}>
            <Link to="/account" onClick={() => handleLinkClick("/account-page")}>Account</Link>
          </li>
        </ul>
        
        <ul className={styles.navListDown}>
          {!token && <li className={`${styles.navItem} ${activeLink === "/login" ? styles.active : ""}`}>
            <Link to="/login" onClick={() => handleLinkClick("/login")}>Login</Link>
          </li>}
          {!token && <li className={`${styles.navItem} ${activeLink === "/register" ? styles.active : ""}`}>
            <Link to="/register" onClick={() => handleLinkClick("/register")}>Register</Link>
          </li>}
          {token && <li className={`${styles.navItem}`}>
            <Link to="/logout" onClick={() => handleLogout()}>Logout</Link>
          </li>}
        </ul>
      </nav>
    </div>
  );
}

export default Sidebar;