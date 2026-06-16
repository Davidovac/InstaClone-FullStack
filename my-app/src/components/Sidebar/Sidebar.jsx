import React from "react";
import { NavLink, Link, useNavigate } from "react-router-dom";
import { useAuthStore } from "../../store/useAuthStore";
import styles from './Sidebar.module.scss';
import SearchBar from "../SearchBar/SearchBar";

const Sidebar = ({ setUsers }) => {
  const navigate = useNavigate();
  const { token } = useAuthStore();

  const handleLogout = () => {
    useAuthStore.getState().logout();
    navigate("/login");
  };

  const handleSearchSubmit = (query) => {
    navigate(`/search?query=${encodeURIComponent(query)}`);
  };

  const navLinkClass = ({ isActive }) =>
    `${styles.navItem} ${isActive ? styles.active : ""}`;

  return (
    <div className={styles.container}>
      <nav>
        <ul className={styles.navListUpper}>
          <li>
            <NavLink to="/" className={`${styles.homePageLink} ${navLinkClass}`} end>
              <h2>InstaClone</h2>
            </NavLink>
          </li>
          <SearchBar onSubmit={handleSearchSubmit} />
          {token && <li>
            <NavLink to="/account" className={navLinkClass}>
              Account
            </NavLink>
          </li>}

          {token && <li>
            <NavLink to="/create-post" className={navLinkClass}>
              Create Post
            </NavLink>
          </li>}
        </ul>

        <ul className={styles.navListDown}>
          {!token && (
            <li>
              <NavLink to="/login" className={navLinkClass}>
                Login
              </NavLink>
            </li>
          )}
          {!token && (
            <li>
              <NavLink to="/register" className={navLinkClass}>
                Register
              </NavLink>
            </li>
          )}
          {token && (
            <li className={styles.navItem}>
              <Link to="/logout" onClick={handleLogout}>
                Logout
              </Link>
            </li>
          )}
        </ul>
      </nav>
    </div>
  );
};

export default Sidebar;
