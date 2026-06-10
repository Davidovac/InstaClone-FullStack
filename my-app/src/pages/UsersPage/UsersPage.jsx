import { useSearchParams } from "react-router-dom";
import { useGetUsers } from "../../hooks/useUserQueries";
import styles from "./UsersPage.module.scss";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";

const UsersPage = () => {
  const [searchParams] = useSearchParams();
  const query = searchParams.get("query") ?? "";

  const { data: users, isLoading, isError } = useGetUsers(query);

  if (isLoading) return <LoadingSpinner />;

  return (
    <div>
      {users?.map((user) => (
        <div key={user.id}>{user.username}</div>
      ))}
    </div>
  );
};