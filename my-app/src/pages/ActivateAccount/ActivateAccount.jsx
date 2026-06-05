import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useSearchParams } from "react-router-dom";
import { useActivateAccount } from "../../hooks/useAuthQueries";

const ActivateAccount = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const email = searchParams.get("email");
  const { mutate: activateAccount, isPending: isActivating, isError: isActivationError, error: activationError } = useActivateAccount();

  useEffect(() => {
    if (token && email) {
      activateAccount({ token, email }, {
        onSuccess: () => {
          navigate("/login");
        },
        onError: (error) => {
          alert(`Error: ${error.message}`);
        }
      });
    }
  }, [token, email]);

  return (
    <div id="activate-account-container" style={{ width: '100%', textAlign: 'center', padding: '20px', paddingTop: '30vh' }}>
      {isActivationError && (
        <div className="error">
          {activationError.message}
        </div>
      )}
      <h2>Activate Account</h2>
      <p>Your account is being activated! You will be redirected to the login page shortly.</p>
    </div>
  );
}

export default ActivateAccount;