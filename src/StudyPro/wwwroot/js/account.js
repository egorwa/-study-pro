window.accountHelpers = {
    toggle(id) {
        const el = document.getElementById(id);
        if (el) el.type = el.type === 'password' ? 'text' : 'password';
    },
    checkStrength(inputId) {
        const pwd = document.getElementById(inputId);
        const bar = document.getElementById(inputId + '-strength');
        if (!pwd || !bar) return;
        const v = pwd.value;
        let score = 0;
        if (v.length >= 8) score++;
        if (/[A-Z]/.test(v)) score++;
        if (/[0-9]/.test(v)) score++;
        if (/[^A-Za-z0-9]/.test(v)) score++;
        const colors = ['#dc3545', '#fd7e14', '#ffc107', '#198754'];
        const labels = ['Weak', 'Fair', 'Good', 'Strong'];
        bar.style.width = v ? (score * 25) + '%' : '0';
        bar.style.background = v ? colors[Math.max(score - 1, 0)] : '';
        bar.title = v ? labels[Math.max(score - 1, 0)] : '';
    },
    checkMatch(inputId, confirmId) {
        const pwd = document.getElementById(inputId);
        const confirm = document.getElementById(confirmId);
        if (!pwd || !confirm || !confirm.value) return;
        const match = pwd.value === confirm.value;
        confirm.classList.toggle('is-invalid', !match);
        confirm.classList.toggle('is-valid', match);
    }
};