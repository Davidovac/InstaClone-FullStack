import { FormEvent } from "react";
import styles from "./SearchBar.module.scss";

const SearchBar = ({ onSubmit }) => {
  const handleSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    const value = formData.get("search");
    onSubmit(value.trim());
  };

  return (
    <form className={styles.wrapper} onSubmit={handleSubmit}>
      <input 
        type="text"
        name="search"
        placeholder="Search..."
      />
      <button type="submit">Search</button>
    </form>
  );
}

export default SearchBar;