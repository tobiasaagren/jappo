window.jappoStorage = {
    get: (key) => {
        const val = localStorage.getItem(key);
        return val !== null ? val : null;
    },
    set: (key, value) => localStorage.setItem(key, value),
    remove: (key) => localStorage.removeItem(key),
    keysWithPrefix: (prefix) =>
        Object.keys(localStorage).filter(k => k.startsWith(prefix))
};
