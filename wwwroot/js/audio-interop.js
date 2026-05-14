window.jappoAudio = {
    speak: (text) => {
        if (!window.speechSynthesis) return;
        speechSynthesis.cancel();
        const u = new SpeechSynthesisUtterance(text);
        u.lang = 'ja-JP';
        u.rate = 0.7;
        u.pitch = 1.0;
        u.volume = 1.0;

        // prefer a native ja-JP voice if available
        const voices = speechSynthesis.getVoices();
        const jaVoice = voices.find(v => v.lang === 'ja-JP' && !v.name.includes('Compact'));
        if (jaVoice) u.voice = jaVoice;

        speechSynthesis.speak(u);
    }
};
