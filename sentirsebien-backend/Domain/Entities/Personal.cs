namespace sentirsebien_backend.Domain.Entities
{
    public class Personal : Usuario
    {
        private List<Rol> roles;

        public List<Rol> Roles
        {
            get { return roles; }
        }

        public Personal(int id, string nombre, string email, string contraseña)
            : base(id, nombre, email, contraseña)
        {
            roles = new List<Rol>();
        }

        public void AsignarRol(Rol rol)
        {
            if (!roles.Contains(rol))
            {
                roles.Add(rol);
            }
        }

        public void RemoverRol(Rol rol)
        {
            if (roles.Contains(rol))
            {
                roles.Remove(rol);
            }
        }

        public override string ToString()
        {
            string rolesAsignados = string.Join(", ", roles.Select(r => r.NombreRol));
            return $"{base.ToString()} (Roles: {rolesAsignados})";
        }
    }

}
